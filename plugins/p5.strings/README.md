String manipulation in Phosphorus Five
===============

p5.strings contains the core string manipulation Active Events of Phosphoris Five. Below you can see them all, together with examples
of how to use them.

## [p5.string.index-of] event, the equivalent of "find"

The *[p5.string.index-of]* Active Event, returns the index of some search query, from within a string, or optionally, an expression's value, converted
into a string somehow. It requires a *[src]* argument, which is what to look for. Consider this code.

```
_data:Thomas Hansen is the creator of Phosphorus Five
p5.string.index-of:x:/-?value
  src:Hansen
```

If you evaluate the above p5.lambda, it will result in the following.

```
_data:Thomas Hansen is the creator of Phosphorus Five
p5.string.index-of
  Hansen:int:7
```

As you can see, the original arguments are gone, and the index of the value "Hansen" is returned as a child node. If there was multiple
occurrencies of the value "Hansen", it would result in multiple return values. One for each occurrency of the value "Hansen".

```
_data:Thomas Hansen is the creator of Phosphorus Five. Hansen's email is mr.gaia@gaiasoul.com
p5.string.index-of:x:/-?value
  src:Hansen
```

Which of course results in.

```
_data:Thomas Hansen is the creator of Phosphorus Five. Hansen's email is mr.gaia@gaiasoul.com
p5.string.index-of
  Hansen:int:7
  Hansen:int:49
```

The *[src]* argument, can also be an expression, leading to multiple results. For instance like this.

```
_data:Thomas Hansen is the creator of Phosphorus Five. Hansen's email is mr.gaia@gaiasoul.com
_src
  no1:Phosphorus
  no2:Hansen
p5.string.index-of:x:/../*/_data?value
  src:x:/../*/_src/*?value
```

The above p5.lambda, would result in the following result.

```
_data:Thomas Hansen is the creator of Phosphorus Five. Hansen's email is mr.gaia@gaiasoul.com
_src
  no1:Phosphorus
  no2:Hansen
p5.string.index-of
  Phosphorus:int:32
  Hansen:int:7
  Hansen:int:49
```

As you can see from above, the search is also case-sensitive, since it doesn't give a match for "phosphorus" within the email address. If
you wish to do a case-insensitive search, you need to resort to a regular expression type of source. Before we look at that though, let's look at how
we can use multiples static sources. Instead of having an expression as the value of *[src]*. One way to achieve that would look like this.

```
_data:Thomas Hansen is the creator of Phosphorus Five. Hansen's email is mr.gaia@gaiasoul.com
p5.string.index-of:x:/../*/_data?value
  src
    no1:Phosphorus
    no2:Hansen
```

The above of course, would result in the exact same result, as the previous version, having an expression as its *[src]*.

### Regular expressions in [p5.string.index-of]

You can also use a regular expression as your source. Consider this code.

```
_data:Thomas Hansen are the same letter as hansen, thomas
p5.string.index-of:x:/-?value
  src:regex:/hansen/i
```

The above matches both occurrencies of "Hansen", since the regex we supplied, is created with the "case-insensitive" flag set to true. Any regular 
expressions you wish to use, can be used here, the above is just a simple example. Though, if you create very complex regular expressions, containing
for instance a ":", etc, you might have to put your regular expression value, into quotes, or double quotes. You can also use multiple regular
expressions as your source.

```
_data:Thomas Hansen are the same letter as hansen, thomas SAID Marin sami people
p5.string.index-of:x:/-?value
  src
    no1:regex:/hansen/i
    no2:regex:/\W+sa[a-zA-Z]{2}\W{1}/i
```

You can also have your source being any Active Event you wish, that somehow returns one or more valid sources for *[p5.string.index-of]*. Consider this.

```
_data:Thomas Hansen are the same letter as hansen, thomas SAID Marin sammens
p5.string.index-of:x:/-?value
  eval
    return:hansen
```

Or have it return multiple sources.

```
_data:Thomas Hansen are the same letter as hansen, thomas SAID Marin sammens
p5.string.index-of:x:/-?value
  eval
    return
      no1:hansen
      no2:thomas
```

Notice how, if you use a node as a source, that the Active Event returns the indexes for each match, as the value of a node, having the name 
from your original node. Notice the different result from these two invocations.

```
_data:Thomas Hansen is the creator of Phosphorus Five. Hansen's email is mr.gaia@gaiasoul.com
_src
  no1:Phosphorus
  no2:Hansen
p5.string.index-of:x:/../*/_data?value
  src:x:/../*/_src/*?value
p5.string.index-of:x:/../*/_data?value
  src:x:/../*/_src/*?node
```

This occurs since the first invocation uses "?value" as the type declaration for your source, while the last invocation uses "?node". This
allows you to "group" together complex results, from different sources, where for instance the source is a regular expression, resulting
in having a "friendly name" for your grouped results. The advantage becomes more easily seen if you exchange the above source to regular
expression sources.

```
_data:Thomas Hansen is the creator of Phosphorus Five. Hansen's email is mr.gaia@gaiasoul.com
_src
  no1:regex:/phosphorus/i
  no2:regex:/Hansen/i
  no3-string:Five
p5.string.index-of:x:/../*/_data?value
  src:x:/../*/_src/*?value
p5.string.index-of:x:/../*/_data?value
  src:x:/../*/_src/*?node
```

As you can see, in the last invocation, the results are "grouped" by their "friendly names", which are "no1" and "no2",  etc. This is often, 
with more complex regular expression source invocations, easier to both read, and handle afterwards.

## [p5.string.join], joining things to strings

The *[p5.string.join]* Active Event, allows you to p5.string.join the results of an expression for instance, into a single string value. Consider this code.

```
_data
  foo1:Hello
  foo2:" "
  foo3:World
p5.string.join:x:/-/*?value
```

The above would produce "Hello World" after being evaluated. You can also pass in two optionally arguments, *[wrap]* and *[sep]*, which are
"wrapper character(s)" and "separator". The separator is a piece of string, which is added between your entries, while the "wrapper" only
makes sense if you also supply a "separator", since it wraps each entity. Consider this code.

```
_data
  foo1:bar1
  foo2:bar2
  foo3:bar3
p5.string.join:x:/-/*?value
  sep:,
  wrap:'
```

The above p5.lambda, will produce the following result.

```
_data
  foo1:bar1
  foo2:bar2
  foo3:bar3
p5.string.join:'bar1','bar2','bar3'
```

Which is useful, if you are for instance creating CSV files, or something similar. Notice that both the value of *[p5.string.join]*, and both of its
arguments, can be both constants and expressions. You can also p5.string.join results of expressions yielding multiple results. Consider this.


```
_data
  foo1:bar1
  foo2:bar2
_data
  foo3:bar3
  foo4:bar4
p5.string.join:x:/../*/_data/*?value
  sep:,
  wrap:'
```

In the above p5.lambda, the expression given to *[p5.string.join]*, will yield multiple results. The Active Event doesn't care, bu treats it as a single
result.

## [length], figuring out the length of a string

The Active Event *[length]*, simply returns the length of a string. If it is given something which is not a string, it will convert it into 
a string, and return its length anyway, as if it was a string. Consider this code.

```
_data:This is a string
length:x:/-?value
```

Which of course yields the value "16". Like *[p5.string.join]*, you can give it an expression leading to multiple results.

## [p5.string.match], regular expression matching some string

The *[p5.string.match]* Active Event is similar to *[p5.string.index-of]* in its regular expression implementation, except instead of returning the index of some
string, it returns the string it matches. Consider this code.

```
_data:Thomas Hansen
p5.string.match:x:/-?value
  src:regex:/hansen/i
```

Which will return the following.

```
_data:Thomas Hansen
p5.string.match
  Hansen
```

Notice the _"i"_ parameter passed into the regular expression, informing it that we want to perform a case-insensitive search.

The *[match]* Active Event will also automatically convert anything in its value to a single string. Just like *[length]* and *[p5.string.join]* will.

## [p5.string.replace], replacing entities of one string with something else

The *[p5.string.replace]* Active Event takes a source, and a *[dest]* argument. It basically replaces every occurrency of some source string, found
in the expression or constant of its value, with whatever you supply as a *[dest]* argument, and return it immutably as the value of *[p5.string.replace]*.
Consider this code.

```
_data:Phosphorus Five is a stupid framework
p5.string.replace:x:/-?value
  src:stupid
  dest:cool
```

All arguments to it, can be either expressions or constants. The *[dest]* argument is optional, and if not supplied, each *[src]* found, will
be replaced an empty string. You can also use Active Event sources, such as the following illustrates.

```
_data:Thomas Hansen is a stupid man
p5.string.replace:x:/-?value
  eval
    return:a stupid
  eval
    return:an intelligent
```

If you use Active Event sources, then your first Active Event invocation, will be considered your *[src]*, and the second its *[dest]*. In
addition, you can have multiple sources defined, such as the following illustrates.

```
_data:Thomas Hansen is a retarded and a stupid man
_src
  no1:a stupid
  no2:a retarded
p5.string.replace:x:/-2?value
  src:x:/../*/_src/*?value
  eval
    return:an intelligent
```

However, there can be only one "destination" value. Above you see how you can combine Active Event sources with expression destinations. Vice
versa is of course also possible.

Notice the "destination" is not really the destination, since this is defined as the value of the *[replace]* node itself - But rather the "what to 
substitute your matches with". Think of it as the "destination for what to replace with" to get the vocabulary correct.






