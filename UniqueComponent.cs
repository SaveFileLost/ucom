using System.Linq;
using System.Collections.Generic;
using Sandbox;

namespace Ucom;

public abstract partial class UniqueComponent : EntityComponent
{
	static int NextUniqueId = 1;
	static Dictionary<int, UniqueComponent> Components = new();

	/// <summary>
	/// Acquires a UniqueComponent from its ID
	/// </summary>
	public static T FromId<T>(int id) where T : UniqueComponent
		=> Components.ContainsKey(id) ? Components[id] as T : null;

	/// <summary>
	/// The unique ID of this component
	/// </summary>
	[Net] public int UniqueId { get; private set; }

	/// <summary>
	/// Make sure to call this in your child class
	/// </summary>
	protected override void OnActivate()
	{
		// The component is being reactivated or replicated to the client
		if (UniqueId != default)
		{
			AddToDict();
			return;
		}

		if (!Game.IsServer) return;

		UniqueId = NextUniqueId;
		NextUniqueId += 1;
		AddToDict();
	}

	/// <summary>
	/// Make sure to call this in your child class
	/// </summary>
	protected override void OnDeactivate() => Components.Remove(UniqueId);
	void AddToDict() => Components.Add(UniqueId, this);
}

public abstract class UniqueComponent<T> : UniqueComponent where T : Entity
{
	new public T Entity => base.Entity as T;
}