//
// CodeBehindTypeNameCache.cs
//
// Author:
//       Michael Hutchinson <mhutch@xamarin.com>
//
// Copyright (c) 2014 Xamarin Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.IO;
using MonoDevelop.Core;

using MonoDevelop.Ide.TypeSystem;
using MonoDevelop.Ide;
using MonoDevelop.AspNet.WebForms.Parser;
using MonoDevelop.AspNet.WebForms;
using MonoDevelop.AspNet.Projects;

namespace MonoDevelop.AspNet.WebForms
{
	class WebFormsCodeBehindTypeNameCache : ProjectFileCache<AspNetAppProject,string>
	{
		public WebFormsCodeBehindTypeNameCache (AspNetAppProject proj) : base (proj)
		{
		}

		protected override string GenerateInfo (string filename)
		{
			try {
				var doc = TypeSystemService.ParseFile (filename, DesktopService.GetMimeTypeForUri (filename), File.ReadAllText (filename)) as WebFormsParsedDocument;
				if (doc != null && !string.IsNullOrEmpty (doc.Info.InheritedClass))
					return doc.Info.InheritedClass;
			} catch (Exception ex) {
				LoggingService.LogError ("Error reading codebehind name for file '" + filename + "'", ex);
			}
			return null;
		}

		public string GetCodeBehindTypeName (string fileName)
		{
			return Get (fileName);
		}
	}
}