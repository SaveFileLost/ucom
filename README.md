# sfl.ucom
Adds support for creating uniquely identifiable components for all intents and purposes.

1. Inherit from `Ucom.UniqueComponent`
```cs
public class MyComponent : Ucom.UniqueComponent
{
}
```

2. If/when defining `OnActivate` and `OnDeactivate` callbacks, make sure to call the base methods
```cs
public class MyComponent : Ucom.UniqueComponent
{
  protected override void OnActivate()
  {
    base.OnActivate();
  }

  protected override void OnDeactivate()
  {
    base.OnDeactivate();
  }
}
```
You can now get the unique identifier from this component and the other way around
```cs
public class MyComponent : Ucom.UniqueComponent
{
  protected override void OnActivate()
  {
    base.OnActivate();

    var myId = this.UniqueId;
    var me = UniqueComponent.FromId<MyComponent>(myId);
  }
}
```
# Support policy
The library will be maintained until its functionality is natively added to S&box.
