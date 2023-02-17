using DataProcessing.Services;
using DataProcessingService.Services;
using Microsoft.Extensions.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<MainWorker>();
        services.AddSingleton<ProcessingFilesService>();
        services.AddSingleton<ProcessingOneFileService>();
        services.AddSingleton<TrackingTimeForMetaService>();
    })
    .Build();
host.Run();



public class MainWorker : IHostedService
{
    protected readonly IConfiguration configuration;
    protected readonly IHostApplicationLifetime hostApplicationLifetime;
    protected readonly ProcessingFilesService processingFilesService;
    protected readonly ProcessingOneFileService processingOneFileService;
    protected readonly TrackingTimeForMetaService trackingTimeForMetaService;
    FileSystemWatcher? watcher;
    public MainWorker(IHostApplicationLifetime hostApplicationLifetime, IConfiguration configuration, ProcessingFilesService processingFilesService, ProcessingOneFileService processingOneFileService, TrackingTimeForMetaService trackingTimeForMetaService)
    {
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.configuration = configuration;
        this.processingFilesService = processingFilesService;
        this.processingOneFileService = processingOneFileService;
        this.trackingTimeForMetaService = trackingTimeForMetaService;
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
            Thread myThread1 = new Thread(trackingTimeForMetaService.TrackingTimeForMeta);
            myThread1.Start();

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