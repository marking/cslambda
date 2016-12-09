using System;
using System.IO;
using System.Text;

using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using Amazon.Lambda.S3Events;

namespace cslambda
{
    public class Function
    {
        public void StreamHandler(Stream input, ILambdaContext context)
        {
                using (var rdr = new StreamReader(input))
                {
                    context.Logger.LogLine($"received {rdr.ReadToEnd()} as input");
                }
                   
        }

        public void S3Handler(S3Event s3Event, ILambdaContext context) 
        {
            context.Logger.LogLine($"Processing {s3Event.Records.Count} records ...");
            foreach (var rec in s3Event.Records)
            {
                context.Logger.LogLine($"{rec.S3.Object.Key} changed in {rec.S3.Bucket.Name}");
            }
            
        }

        //public void FunctionHandler(KinesisEvent kinesisEvent, ILambdaContext context)
        //{
        //    context.Logger.LogLine($"Beginning to process {kinesisEvent.Records.Count} records...");

        //    foreach (var record in kinesisEvent.Records)
        //    {
        //        context.Logger.LogLine($"Event ID: {record.EventId}");
        //        context.Logger.LogLine($"Event Name: {record.EventName}");

        //        string recordData = GetRecordContents(record.Kinesis);
        //        context.Logger.LogLine($"Record Data:");
        //        context.Logger.LogLine(recordData);
        //    }

        //    context.Logger.LogLine("Stream processing complete.");
        //}

        private string GetRecordContents(KinesisEvent.Record streamRecord)
        {
            using (var reader = new StreamReader(streamRecord.Data, Encoding.ASCII))
            {
                return reader.ReadToEnd();
            }
        }
    }
}