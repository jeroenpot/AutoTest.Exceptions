AutoTest.Exception
==================
Unittest all the custom Exceptions in your assembly against the microsoft design guidelines
 - Does it have all required constructors
 - Can it be serialized and deserialize
 - Are the constructor arguments set

For more information abpit the design guidelines see http://msdn.microsoft.com/en-us/library/vstudio/ms229014(v=vs.100).aspx

##Usage##

- Create a unittest project for your project and make a test class
- Reference this package and include this code (Nunit Example)

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
