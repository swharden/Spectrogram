using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram.Tests
{
    class Wspr
    {
        [Test]
        public void Test_wspr_reader()
        {
            string testFolderPath = @"C:\Users\Scott\Documents\temp\wsprTest\ALL_WSPR.TXT";
            var reader = new ArgoNot.WsprLogWatcher(testFolderPath);
        }
    }
}
