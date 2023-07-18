using System.Linq;
using System.Collections.Generic;
using Sandbox;

namespace Ucom;

public abstract partial class UniqueComponent : EntityComponent
{
	static uint NextUniqueId = 1;
	static Dictionary<uint, UniqueComponent> Components = new();

	public static T FromId<T>(uint id) where T : UniqueComponent
		=> Components.ContainsKey(id) ? Components[id] as T : null;

	[Net] public uint UniqueId { get; private set; }

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