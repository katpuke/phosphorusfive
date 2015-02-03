
/*
 * phosphorus five, copyright 2014 - Mother Earth, Jannah, Gaia
 * phosphorus five is licensed as mit, see the enclosed LICENSE file for details
 */

using System;
using System.Collections.Generic;
using phosphorus.core;

namespace phosphorus.lambda.iterators
{
    /// <summary>
    /// iterator class, wraps one iterator for hyperlisp expressions.  iterators are put in chain, encapsulated within a
    /// <see cref="phosphorus.execute.iterator.Logical"/> object, which again is a child of <see cref="phosphorus.execute.iterator.IteratorGroup"/>, 
    /// which again is an iterator.  when iterators are evaluated, they're evaluated as last in, where the last iterates on the evaluated
    /// result of its previous or "Left" iterator, recursively, until root iterator is reached, which normally should return a single
    /// <see cref="phosphorus.core.Node"/>, which at the end is the input to the iterators
    /// </summary>
    public abstract class Iterator
    {
        private Iterator _left;

        /// <summary>
        /// gets or sets the left or "previous iterator"
        /// </summary>
        /// <value>its previous iterator in the chain of iterators</value>
        public Iterator Left {
            get {
                return _left;
            }
            set {
                _left = value;
            }
        }

        /// <summary>
        /// evaluates the iterator
        /// </summary>
        /// <value>the evaluated result returning a list of <see cref="phosphorus.core.Node"/>s</value>
        public abstract IEnumerable<Node> Evaluate {
            get;
        }

        /// <summary>
        /// returns true if this is a "reference expression"
        /// </summary>
        /// <value>true if this is a reference expression</value>
        public virtual bool Reference {
            get;
            set;
        }
    }
}