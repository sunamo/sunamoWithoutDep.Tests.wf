using System;
using System.Drawing;
using System.IO;
using sunamo.Essential;
using Xunit;

namespace SunamoIco.Tests
{
    public class SunamoIcoHelperTests
    {
        [Fact]
        public void SunamoIcoAll()
        {
            ThisApp.Name = "SunamoIco.Tests";
            AppData.ci.CreateAppFoldersIfDontExists();

            var input = @"E:\vs\Projects\sunamo.cz\sunamo.cz\_\i\Apps\IconsOfApp\12.png";
            var folder = AppData.ci.GetFolder(AppFolders.Output);
            Bitmap bmp = new Bitmap(input);

            Save(input, folder, bmp, SunamoIcoHelper.ConvertToIco, "ConvertToIco");
            //Save(input, folder, bmp, SunamoIcoHelper.ConvertToIcon, "ConvertToIcon");
            Save(input, folder, bmp, SunamoIcoHelper.IconFromImage, "IconFromImage");
            //Save(input, folder, SunamoIcoHelper.ConvertToIco, "");
            //Save(input, folder, SunamoIcoHelper.ConvertToIco, "");
        }

        private void Save(string input, string folder, Bitmap bmp, Func<Image, Icon> convertToIco, string v)
        {
            var f = FS.Combine(folder, v + AllExtensions.ico);

            var icon = convertToIco.Invoke(bmp);
            using (FileStream fs = new FileStream(f, FileMode.OpenOrCreate))
            { 
                icon.Save(fs);
            }
        }
    }
}
