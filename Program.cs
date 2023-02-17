using DataProcessingService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<MainWorker>();
        services.AddSingleton<ProcessingFilesService>();
        services.AddSingleton<ProcessingOneFileService>();
    })
    .Build();
host.Run();



public class MainWorker : IHostedService
{
    protected readonly IConfiguration configuration;
    protected readonly IHostApplicationLifetime hostApplicationLifetime;
    protected readonly ProcessingFilesService processingFilesService;
    protected readonly ProcessingOneFileService processingOneFileService;
    FileSystemWatcher? watcher;
    public MainWorker(IHostApplicationLifetime hostApplicationLifetime, IConfiguration configuration, ProcessingFilesService processingFilesService, ProcessingOneFileService processingOneFileService)
    {
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.configuration = configuration;
        this.processingFilesService = processingFilesService;
        this.processingOneFileService = processingOneFileService;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var folderA = configuration.GetSection("path:folderA").Value;
        var folderB = configuration.GetSection("path:folderB").Value;
        if (folderA == null || folderB == null)
            hostApplicationLifetime.StopApplication();
        else
        {
            watcher = new FileSystemWatcher(folderA);

            int counFilesInWork = Directory.GetFiles(folderA).Length;
            if (counFilesInWork > 0)
                await processingFilesService.ProcessingFiles(Directory.GetFiles(folderA));

            watcher.Created += async (obj, file) => await processingOneFileService.ProcessingOneFile(file.FullPath);

            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            await Task.CompletedTask;
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        watcher?.Dispose();
        await Task.CompletedTask;
    }
}