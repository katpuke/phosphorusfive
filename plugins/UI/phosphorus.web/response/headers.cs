
/*
 * phosphorus five, copyright 2014 - Mother Earth, Jannah, Gaia
 * phosphorus five is licensed as mitx11, see the enclosed LICENSE file for details
 */

using System;
using System.Web;
using phosphorus.core;
using phosphorus.lambda;

namespace phosphorus.web
{
    /// <summary>
    /// helper to manipulate the HTTP response
    /// </summary>
    public static class headers
    {
        /// <summary>
        /// changes or removes existing HTTP headers, or adds new HTTP headers to response
        /// </summary>
        /// <param name="context"><see cref="phosphorus.Core.ApplicationContext"/> for Active Event</param>
        /// <param name="e">parameters passed into Active Event</param>
        [ActiveEvent (Name = "pf.web.headers.set")]
        private static void pf_web_headers_set (ApplicationContext context, ActiveEventArgs e)
        {
            if (e.Args.Count == 0) {

                // "remove headers" invocation, looping through all headers user wish to remove
                XUtil.Iterate<string> (e.Args, 
                delegate (string idx) {
                    HttpContext.Current.Response.Headers.Remove (idx);
                });
            } else {

                // adding header(s) invocation
                string value = XUtil.Single (e.Args.LastChild, "; ");
                XUtil.Iterate<string> (e.Args, 
                delegate (string idx) {
                    HttpContext.Current.Response.AddHeader (idx, value);
                });
            }
        }
    }
}