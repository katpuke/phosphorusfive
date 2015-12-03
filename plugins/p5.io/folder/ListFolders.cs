/*
 * Phosphorus Five, copyright 2014 - 2015, Thomas Hansen, phosphorusfive@gmail.com
 * Phosphorus Five is licensed under the terms of the MIT license, see the enclosed LICENSE file for details
 */

using System.IO;
using p5.core;
using p5.exp;

namespace p5.io.folder
{
    /// <summary>
    ///     Class to help list all folders within folder
    /// </summary>
    public static class ListFolders
    {
        /// <summary>
        ///     List all folders in folder
        /// </summary>
        /// <param name="context">Application context</param>
        /// <param name="e">Parameters passed into Active Event</param>
        [ActiveEvent (Name = "list-folders")]
        private static void p5_folder_list_folders (ApplicationContext context, ActiveEventArgs e)
        {
            // Making sure we clean up and remove all arguments passed in after execution
            using (new Utilities.ArgsRemover (e.Args, true)) {

                // Retrieving root folder
                var rootFolder = Common.GetRootFolder (context);

                // Iterating through each folder passed in by caller
                foreach (var idxFolder in Common.GetSource (e.Args, context)) {

                    // Verifying user is authorized to reading from currently iterated folder
                    context.Raise ("_authorize-load-folder", new Node ("_authorize-load-folder", idxFolder).Add ("args", e.Args));

                    // Iterating all folders in current directory, and returning as nodes beneath args given
                    foreach (var idxInnerFolder in Directory.GetDirectories (rootFolder + idxFolder)) {

                        // Normalizing file path delimiters for both Linux and Windows
                        var folderName = idxInnerFolder.Replace ("\\", "/");
                        folderName = folderName.Replace (rootFolder, "");
                        e.Args.Add (new Node ("/" + folderName.Trim ('/') + "/"));
                    }
                }
            }
        }
    }
}
