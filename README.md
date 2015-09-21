[![Build status](https://ci.appveyor.com/api/projects/status/h0vo52hogp69ju2t?svg=true)](https://ci.appveyor.com/project/jeroenpot/autotest-exceptions)
[![Coverage Status](https://coveralls.io/repos/jeroenpot/AutoTest.Exceptions/badge.svg?branch=&service=github)](https://coveralls.io/github/jeroenpot/AutoTest.Exceptions?branch=)

AutoTest.Exception
==================
Unittest all the custom Exceptions in your assembly against the microsoft design guidelines
 - Does it have all required constructors
 - Can it be serialized and deserialized
 
For more information about the design guidelines see http://msdn.microsoft.com/en-us/library/vstudio/ms229014(v=vs.100).aspx

##Usage##

- Create a unittest project for your project and make a test class
- Install the nuget package from https://www.nuget.org/packages/AutoTest.Exceptions/

Or install via package console.

```sh
Install-Package AutoTest.Exceptions
```

```sh
[TestFixture]
public class ExampleTest
{
    [Test]
    public void TestAllMyCustomExceptions()
    {
        ExceptionTester.TestAllExceptions(Assembly.GetAssembly(typeof(AClassInTheAssemblyIWantToTest)));
    }
}
```

## License

The MIT License (MIT)

Copyright (c) 2014 Jeroen Pot

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
