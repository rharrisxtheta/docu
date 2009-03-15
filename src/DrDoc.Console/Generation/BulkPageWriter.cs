﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DrDoc.Documentation;

namespace DrDoc.Generation
{
    public class BulkPageWriter : IBulkPageWriter
    {
        private readonly IPageWriter writer;

        public BulkPageWriter(IPageWriter writer)
        {
            this.writer = writer;
        }

        public void CreatePagesFromDirectory(string templatePath, string destination, IList<Namespace> namespaces)
        {
            writer.SetTemplatePath(templatePath);

            foreach (var file in Directory.GetFiles(templatePath, "*.spark", SearchOption.AllDirectories))
            {
                if (IsPartial(file)) continue;

                writer.CreatePages(file, destination, namespaces);
            }
        }

        private bool IsPartial(string file)
        {
            return Path.GetFileName(file).StartsWith("_");
        }
    }
}
