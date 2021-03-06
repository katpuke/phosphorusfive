/*
 * Phosphorus Five, copyright 2014 - 2017, Thomas Hansen, thomas@gaiasoul.com
 * 
 * This file is part of Phosphorus Five.
 *
 * Phosphorus Five is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License version 3, as published by
 * the Free Software Foundation.
 *
 *
 * Phosphorus Five is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Phosphorus Five.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * If you cannot for some reasons use the GPL license, Phosphorus
 * Five is also commercially available under Quid Pro Quo terms. Check 
 * out our website at http://gaiasoul.com for more details.
 */

using System;
using p5.ajax.core;

namespace p5.samples
{
    using p5 = ajax.widgets;

    public class JavaScript : AjaxPage
    {
        [WebMethod]
        protected void javascript_widget_onclicked (p5.Literal literal, EventArgs e)
        {
            // Here we extract the "custom_data" pushed from our JavaScript handler on the client side,
            // and prepend that string to the server-time. before returning the combined results to our client again as "custom_return_value".
            SendObject ("custom_return_data", Page.Request.Params["custom_data"] + "Your server speaks the server-time " + DateTime.Now + ".\r\n");
        }
    }
}