## pwither.ev - Решение для вызова событий (методов) из статического класса или использования наследования от класса скрипта с использованием атрибутов.
### Документация
#### Атрибуты
Вы можете присваивать своим методам атрибуты события при помощи стандартного атрибута LocalEvent, он стандартно использует для идентификации событий string:
```csharp
[LocalEvent(<eventId>)]
public void SomeMethod()
{
	//some code
}
```
Также можно использовать атрибут, в котором Id события представлен обобщенным типом (Generics), создадим для демонстрации перечисление SomeEnum:
```csharp
enum SomeEnum
{
	One,
	Two,
	Three
}

[GenericEvent<SomeEnum>(<eventId>)]
public void SomeMethod()
{
	//some code
}
```
Использовать GenericEvent не всегда удобно, поэтому возможно создать свой атрибут с уже приведенным универсальным типом:
```csharp
enum SomeEnum
{
	One,
	Two,
	Three
}

internal class EnumEventAttribute : GenericEventAttribute<SomeEnum>
{
    public EnumEventAttribute(SomeEnum id) : base(id)
    {
    }
}

[EnumEvent(<eventId>)]
public void SomeMethod()
{
    //some code
}
```
#### Срипты
Для вызова методов могут использоваться как статические классы, так и скрипты. Для LocalEvent используется стандартный класс EventScript:
```csharp
public class SomeClass : EventScript
{
    [LocalEvent(<eventId>)]
    public void SomeMethod()
    {
        //some code
    }
}
```
Пример вызова события для EventScript:
```csharp
var obj = new SomeClass();
obj.InvokeEvent(<eventId>);
```
Для универсальных типов используется GenericEventScript:
```csharp
enum SomeEnum
{
	One,
	Two,
	Three
}

public class SomeClass : GenericEventScript<SomeEnum>
{
    [GenericEvent<SomeEnum>(<eventId>)]
    public void SomeMethod()
    {
        //some code
    }
}
```
Но это также не всегда удобно, поэтому можно использовать наследование:
```csharp
public class EnumEventScript : GenericEventScript<SomeEnum>
{
}

internal class SomeClass : EnumEventScript
{
    [EnumEvent(<eventId>)]
    public void SomeMethod()
    {
        //some code
    }
}
```
Медоты, имеющие параметры вызываются следующим образом:
```csharp
public class SomeClass : EventScript
{
    [LocalEvent("someId")]
    public void SomeMethod(int someArg)
    {
        Console.WriteLine(someArg);
    }
}

var obj = new SomeClass();
//передача параметров осуществляется через InvokeEvent:
obj.InvokeEvent("someId", 1);	//output 1
obj.InvokeEvent("someId", 2);	//output 2
obj.InvokeEvent("someId", 3);	//output 3
```
Это также применимо для любого количества параметров и любого типа скриптов.
#### Статические диспетчеры
С помощью диспетчеров можно регистрировать методы объекта и вызывать их из любой точки программы без использования скриптов. Существует 2 диспетчера - для LocalEventAttribute и для GenericEventAttribute: EventDispatcher и GenericEventDispatcher соответственно. Для GenericEventDispatcher можно создавать свои объекты, и пользоваться ими из него.
Пример использования EventDispatcher:

```csharp
public class SomeClass
{
    [LocalEvent("someId")]
    public void SomeMethod()
    {
        //some code
    }
	[LocalEvent("output")]
    public void OutputMethod(int someArg)
    {
        Console.WriteLine(someArg);
    }
}

var obj = new SomeClass();
EventDispatcher.Register(obj);
EventDispatcher.Invoke("someId");
EventDispatcher.Invoke("output", 1); //output 1
EventDispatcher.Invoke("output", 2); //output 2
EventDispatcher.Unregister(obj);
```
Пример использования GenericEventDispatcher:
```csharp
enum SomeEnum
{
	One,
	Two,
	Three
}

public class SomeClass
{
    [GenericEvent<SomeEnum>(SomeEnum.One)]
    public void SomeMethod()
    {
        //some code
    }
	[GenericEvent<SomeEnum>(SomeEnum.Two)]
    public void OutputMethod(int someArg)
    {
        Console.WriteLine(someArg);
    }
	[GenericEvent<SomeEnum>(SomeEnum.Three)]
    public void OutputMethod(int someArg1, string someArg2)
    {
        Console.WriteLine(someArg1 + someArg2);
    }
}

var obj = new SomeClass();
GenericEventDispatcher<SomeEnum>.Register(obj);
GenericEventDispatcher<SomeEnum>.Invoke(SomeEnum.One);
GenericEventDispatcher<SomeEnum>.Invoke(SomeEnum.Two, 1); //output 1
GenericEventDispatcher<SomeEnum>.Invoke(SomeEnum.Three, 1, "someText"); //output 1someText
GenericEventDispatcher<SomeEnum>.Unregister(obj);
```
Пример использования наследуемых классов, включая диспетчера:
```csharp
enum SomeEnum
{
    One,
    Two,
    Three
}

public class EnumDispatcher : GenericEventDispatcher<SomeEnum>
{
}

public class EnumEventAttribute : GenericEventAttribute<SomeEnum>
{
    public EnumEventAttribute(SomeEnum id) : base(id)
    {
    }
}

public class SomeClass
{
    [EnumEvent(SomeEnum.One)]
    public void SomeMethod()
    {
        //some code
    }
	[EnumEvent(SomeEnum.Two)]
    public void OutputMethod(int someArg)
    {
        Console.WriteLine(someArg);
    }
	[EnumEvent(SomeEnum.Three)]
    public void OutputMethod(int someArg1, string someArg2)
    {
        Console.WriteLine(someArg1 + someArg2);
    }
}

var obj = new SomeClass();
EnumDispatcher.Register(obj);
EnumDispatcher.Invoke(SomeEnum.One);
EnumDispatcher.Invoke(SomeEnum.Two, 1); //output 1
EnumDispatcher.Invoke(SomeEnum.Three, 1, "someText"); //output 1someText
EnumDispatcher.Unregister(obj);
```
Методы любого диспетчера сводятся к:
```csharp
public static void Register(object handler)	//регистрирует объект в системе
public static void Unregister(object handler)	//удаляет объект из системы
public static void UnregisterAll()	//удаляет все объекты из системы
public static void RegisterByEventId(object handler, string id)	//регистрирует объект для идентификатора определенного события
public static void UnregisterByEventId(object handler, string id)	//удаляет объект для идентификатора определенного события
public static void UnregisterAllByEventId(string id)	//удаляет все объекты, присущие определенному идентификатору события
public static void Invoke(string id, params object[] eventArgs)	//вызывает собитие
```
