﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace TurboJpegWrapper.Tests
{
    static class TestUtils
    {
        public static IEnumerable<Bitmap> GetTestImages(string searchPattern)
        {
            var imagesDir = Path.Combine(BinPath, "images");

            foreach (var file in Directory.EnumerateFiles(imagesDir, searchPattern))
            {
                Bitmap bmp;
                try
                {
                    bmp = (Bitmap)Image.FromFile(file);
                    Debug.WriteLine($"Input file is {file}");
                }
                catch (OutOfMemoryException)
                {
                    continue;
                }
                catch (IOException)
                {
                    continue;
                }
                yield return bmp;
            }
        }

        public static IEnumerable<Tuple<string, byte[]>> GetTestImagesData(string searchPattern)
        {
            var imagesDir = Path.Combine(BinPath, "images");

            foreach (var file in Directory.EnumerateFiles(imagesDir, searchPattern))
            {
                Debug.WriteLine($"Input file is {file}");
                yield return new Tuple<string, byte[]>(file, File.ReadAllBytes(file));
            }
        }

        public static string BinPath
        {
            get
            {
                Assert.IsTrue(TurboJpegImport.LibraryFound);
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
        }
    }
}
