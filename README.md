SimpleFixture
=============

SimpleFixture is a .net library that helps provide test data for unit tests and integration tests. The fixture satisfies these three basic use cases. It supports .Net 4.5, .Net Standard 1.0, and PCL Profile 259.

* **Locate** - Creates a new instance of the requested type. It does not populate public properies. 
```C#
var fixture = new Fixture();

var instance = fixture.Locate<SomeClass>();
```
* **Populate** - Populate all public properties with randomly created data for a given instance
```C#
var fixture = new Fixture();

fixture.Populate(instance);
```
* **Generate** - Create a new instance of a given type and populate all public properties.
```C#
var fixture = new Fixture();

var instance = fixture.Generate<SomeClass>();
```

### Return

It's useful sometimes to control what values are returned from the fixture. The Return method offers a way to specify what to return for a give type. Below are some examples.

```C#
var fixture = new Fixture();

// return the value 1 whenever an int is needed
fixture.Return(1);

// return the sequence 1, 2, 3, 1, 2, 3 when an int is requested 
fixture.Return(1, 2, 3);

// return incrementing sequence when an int is requested.
int i = 1;
fixture.Return(() => i++);
```

Return also offers the ability to be more granular when returning as seen in the examples below. Note you can chain return filters together to be even more granular
```C#
var fixture = new Fixture();

// return the value 1 when being used to construct SomeClass
fixture.Return(1).For<SomeClass>();

// return incrementing value for int fields ending in Id
int i = 1;
fixture.Return(() => i++).WhenNamed(n => n.EndsWith("Id"));

// return "SomeString" for SomeClass and matchingMethod returns true
fixture.Return("SomeString").For<SomeClass>().WhenMatching(matchingMethod);

```
### Export
Similar to a depenedency injection container you can specify an implementation for a particular interface

```C#
var fixture = new Fixture();

// Export one class at a time
fixture.ExportAs<SomeClass, ISomeInterface>();
var instance = fixture.Locate<ISomeInterface>();
Assert.InstanceOf<SomeClass>(instance);

// Export all classes located in the same assmelby as SomeClass
fixture.ExportAllByInterface().FromAssemblyContaining<SomeClass>();
var instance = fixture.Locate<ISomeInterface>();
Assert.InstanceOf<SomeClass>(instance);
```

### Constraints

Sometimes it's useful to have more control over how an object is created. The generate method takes a constraints object allowing to specify min,max, and other options.
```C#
// Generate an int between 10 and 100
var intValue = fixture.Generate<int>(constraints: new { min = 10, max = 100 });

// Generate constrained date
var dateValue = fixture.Generate<DateTime>(constraints: new { min = dateMin, max = dateMax });

// Set property SomeProperty to value 123 for any int property named SomeProperty
var instance = fixture.Generate<SomeClass>(constraints: new { SomeProperty = 123 });

// _Values allows you to provide values based on type rather than on name
var someClass = new SomeClass { IntValue = 50, StringValue = "Test" };
var instance = fixture.Generate<ImportSomeClass>(constraints: new { _Values = new[] { someClass } });
Assert.Same(someClass, instance.SomeClass);
```

### Customization

Customization offers you the ability to control how an object gets constructed and populated when Return and Constraints aren't enough. 

```C#
var fixture = new Fixture();

// customize how a new instance is created
fixture.Customize<SomeClass>().New(r => new SomeClass());

// customize creation for a new instance with a random int and string
fixture.Customize<SomeOtherClass>().NewFactory<int,string>((i,s) = new SomeOtherClass(i,s,"HardCoded"));

// call the Initialize method everytime a new instance is created
fixture.Customize<SomeClass>().Apply(x => x.Initialize());
```

### Requested Name

To help provide more context on how a type should be create you can provide a request name. Below are some example of currently supported request names for strings

```C#
var fixture = new Fixture();

// generate a first name
var firstName = fixture.Generate<string>("FirstName");

// generate a last name
var lastName = fixture.Generate<string>("LastName");

// generate a random password with 1 Upper, 1 Lower, 1 Special character and a minimium of 8 characters
var password = fixture.Generate<string>("Password");

// generate a random email for the specified domain
var email = fixture.Generate<string>("EmailAddress", constraints: new { domain = "gmail.com" });

// generate an address line 1
var addressLine1 = fixture.Generate<string>("AddressLine1");
```

### Freeze
Similar to Autofixture there is a Freeze method that Generates a new instance and sets it as a Return value.

```C#
// Generate random int and set it as a Return
int randomInt = fixture.Freeze<int>();
Assert.Equal(randomInt, fixture.Generate<int>());

// Freeze an int value for SomeClass
int randomInt = fixture.Freeze<int>(value: i => i.For<SomeClass>());
```

### Behavior
Behavior allows you to apply cross cutting logic to all objects created by the fixture. You can apply your logic to all objects or just to specific types.

```C#
// Execute SomeMethod on every object that is created by the fixture
fixture.Behavior.Add(i => SomeMethod(i));

// Execute behavior on every instance of ISomeType
fixture.Behavior.Add<ISomeType>(i => i.SomeMethodOnISomeType());
```

### Mocking

Currently Moq, NSubstitute and FakeItEasy are supported allowing you to automatically mock any missing interfaces
```C#
// MoqFixture is in SimpleFixture.Moq
var fixture = new MoqFixture();

// SubFixture is in SimpleFixture.NSubstitute
var fixture = new SubFixture();

// FakeFixture is in SimpleFixture.FakeItEasy
var fixture = new FakeFixture();
```

### Builds
[![Build status](https://ci.appveyor.com/api/projects/status/6ml6ubwk7v8u4h9m?svg=true)](https://ci.appveyor.com/project/ipjohnson/simplefixture) [![Build Status](https://travis-ci.org/ipjohnson/SimpleFixture.svg?branch=master)](https://travis-ci.org/ipjohnson/SimpleFixture) [![Coverage Status](https://coveralls.io/repos/github/ipjohnson/SimpleFixture/badge.svg?branch=master)](https://coveralls.io/github/ipjohnson/SimpleFixture?branch=master)
