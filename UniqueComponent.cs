using System.Linq;
using Sandbox;

namespace Ucom;

public abstract partial class UniqueComponent : EntityComponent
{
	static uint NextUniqueId = 0;

	public static T FromId<T>(uint id) where T : UniqueComponent => GetAllOfType<T>().FirstOrDefault(u => u.UniqueId == id);

	[Net] public uint UniqueId { get; private set; }

	public UniqueComponent()
	{
		if (Game.IsServer)
		{
			UniqueId = NextUniqueId;
			NextUniqueId += 1;
		}
	}
}

public abstract class UniqueComponent<T> : UniqueComponent where T : Entity
{
	new public T Entity => base.Entity as T;
}