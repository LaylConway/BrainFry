﻿using System;

namespace BrainFry.Commands
{
	public sealed class LoopOpenCommand : ICommand
	{
		public char Char
		{
			get { return '['; }
		}

		public void Execute(ExecutionContext context)
		{
			if (context.CurrentMemory != 0) // If current memory true (!=0) => execute block
			{
				context.LoopStack.Push(context.CommandPointer);
			}
			else // If current memory false (0) => skip block
			{
				var depth = 0;
				while (true)
				{
					context.CommandPointer++;
					var commandType = context.CurrentCommand.GetType();

					if (commandType == typeof (LoopCloseCommand))
					{
						if (depth == 0) // This is our stop!
							break;

						depth--; // Nested loop close
					}
					else if (commandType == typeof (LoopOpenCommand))
					{
						depth++; // Nested loop open
					}
				}
			}
		}
	}
}