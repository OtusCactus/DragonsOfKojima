using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DragonsOfKojima
{
	public class GetNumberOfMinesInRadius : Conditional
	{
		public override TaskStatus OnUpdate()
		{


			return TaskStatus.Success;
		}
	}
}