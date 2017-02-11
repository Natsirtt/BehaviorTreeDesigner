﻿using UnityEngine;
using NodeEditorFramework;
using System.Collections.Generic;

namespace BehavorTreeDesigner
{
	[Node(false, "Behavior/Composite/Random")]
	public class Random : BaseBehaviorNode
	{
		public override Node Create(Vector2 pos)
		{
			Random node = CreateInstance<Random>();
			base.Init(node);

			node.rect = new Rect(pos.x, pos.y, 140, 80);
			node.CreateInput("In", "Behave", NodeSide.Top, 70);
			node.CreateOutput("Out", "Behave", NodeSide.Bottom, 20);
			node.CreateOutput("Out", "Behave", NodeSide.Bottom, 70);
			node.CreateOutput("Out", "Behave", NodeSide.Bottom, 120);
			
			return node;
		}

		public override NodeStatus Tick(BehaviorBlackboard data)
		{
			List<NodeOutput> nodes = this.Outputs;
			BaseBehaviorNode[] nodeArray = new BaseBehaviorNode[3];
			int count = 0;

			foreach(NodeOutput node in nodes)
			{
				if(node.connections.Count == 0)
					continue;
				
				nodeArray[count] = (BaseBehaviorNode)(node.connections[0].body);
				count ++;
			}

			if(count == 0)
			{
				Debug.LogError("Behavor Tree Designer\nNo output node is connected on node: " + this.name);
				return NodeStatus.ERROR;
			}

			int rand = UnityEngine.Random.Range(0, count);
			NodeStatus childstatus = nodeArray[rand].Tick(data);
			return childstatus;
		}
	}
}