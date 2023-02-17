using DataProcessing.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessingService.Services
{
    public class TrackingTimeForMetaService
    {
        protected readonly IConfiguration configuration;
        public TrackingTimeForMetaService(IConfiguration configuration) {
        this.configuration = configuration;
        }
        public void TrackingTimeForMeta(){
            while (true)
            {
                DateTime dateTime = DateTime.Now;
                if (dateTime.Hour == 17 && dateTime.Minute == 55 && dateTime.Second == 01 && dateTime.Millisecond == 01)
                {
                    WriteMetaDataService.WriteMetaDataToFile(configuration);
                }
            }

        }
    }
}
