
/*
 * phosphorus five, copyright 2014 - Mother Earth, Jannah, Gaia
 * phosphorus five is licensed as mitx11, see the enclosed LICENSE file for details
 */

using System;
using System.Web;
using System.Collections.Generic;
using phosphorus.core;
using phosphorus.expressions;

namespace phosphorus.web.ui
{
    /// <summary>
    /// helper class for classes that requires to set and retrieve values from collections, 
    /// such as Session and Application
    /// </summary>
    public static class CollectionBase
    {
        /// <summary>
        /// callback functor object for Set operation
        /// </summary>
        public delegate void SetDelegate (string key, object value);

        /// <summary>
        /// callback functor object for Get operation
        /// </summary>
        public delegate object GetDelegate (string key);
        
        /// <summary>
        /// callback functor object for List operation
        /// </summary>
        public delegate IEnumerable<string> ListDelegate ();

        /// <summary>
        /// sets a value to a collection
        /// </summary>
        /// <param name="node">root node of collection Active Event</param>
        /// <param name="context">application context</param>
        /// <param name="functor">callback functor, will be invoked once for each key</param>
        public static void Set (Node node, ApplicationContext context, SetDelegate functor)
        {
            // retrieving source
            object source = XUtil.Source (node, context);

            // looping through each destination, creating an object, or removing an existing
            // object, for each destination
            foreach (var idx in XUtil.Iterate<string> (node, context)) {

                // single object
                functor (idx, source);
            }
        }

        /// <summary>
        /// gets a value from a collection
        /// </summary>
        /// <param name="node">root node of collection Active Event</param>
        /// <param name="context">application context</param>
        /// <param name="functor">callback functor, will be invoked once for each key</param>
        public static void Get (Node node, ApplicationContext context, GetDelegate functor)
        {
            // iterating through each "key"
            foreach (var idx in XUtil.Iterate<string> (node, context)) {

                // retrieving object from key
                object value = functor (idx);
                if (value != null) {

                    // adding key node, and value as object, if value is not node, otherwise
                    // appending value nodes beneath key node
                    Node resultNode = node.Add (idx).LastChild;
                    if (value is Node) {

                        // value is Node
                        resultNode.Add ((value as Node).Clone ());
                    } else if (value is IEnumerable<object>) {

                        // value is a bunch of nodes
                        foreach (var idxValue in value as IEnumerable<object>) {
                            if (idxValue is Node)
                                resultNode.Add ((idxValue as Node).Clone ());
                            else
                                resultNode.Add (string.Empty, idxValue);
                        }
                    } else {

                        // value is any "other type of value", returning it anyway, even though it
                        // cannot possibly have come from pf.lambda, to allow user to retrieve "any values"
                        // that exists
                        resultNode.Value = value;
                    }
                }
            }
        }

        public static void List (Node node, ApplicationContext context, ListDelegate functor)
        {
            // retrieving filters, if any
            List<string> filter = new List<string> (XUtil.Iterate<string> (node, context));

            // looping through each existing key in collection
            foreach (var idxKey in functor ()) {

                // returning current key, if it matches our filter, or filter is not given
                if (filter.Count == 0) {

                    // no filter was given
                    node.Add (idxKey);
                } else {

                    // filter was given, checking if key matches one of our filters
                    foreach (string idxFilter in filter) {
                        if (idxKey.IndexOf (idxFilter) != -1) {

                            // matches filter, hence adding to output
                            node.Add (idxKey);
                            break; // no reasons to continue iteration of filters
                        }
                    }
                }
            }
        }
    }
}