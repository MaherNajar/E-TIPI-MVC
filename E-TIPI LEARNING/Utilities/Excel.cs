using E_TIPI_LEARNING.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_TIPI_LEARNING.Utilities
{
    public class Excel
    {
        public static async Task<MemoryStream> Export(IList<LearningRessource> learningRessources, IHostingEnvironment host)
        {
            string webRootFolder = host.WebRootPath;
            string fileName = @"E-TIPI.xlsx";
            string path = Path.Combine(webRootFolder, fileName);

            var memoryStream = new MemoryStream();
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("LearningSummary");

                IRow row = excelSheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("Title");
                row.CreateCell(1).SetCellValue("Description");
                row.CreateCell(2).SetCellValue("Start date");
                row.CreateCell(3).SetCellValue("End date");

                int i = 1;

                foreach(LearningRessource ressource in learningRessources)
                {
                    row = excelSheet.CreateRow(i);
                    row.CreateCell(0).SetCellValue(ressource.Title);
                    row.CreateCell(1).SetCellValue(ressource.Description);
                    row.CreateCell(2).SetCellValue(ressource.StartDate.ToString());
                    row.CreateCell(3).SetCellValue(ressource.EndDate.ToString());

                    i++;
                }

                workbook.Write(fs);
            }

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }

            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
