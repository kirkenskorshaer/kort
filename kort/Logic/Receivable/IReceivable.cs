using Data;
using Logic.Security;
using System.Collections.Generic;

namespace Logic.Receivable
{
	public interface IReceivable<InputType, OutputType>
	{
		List<Privilege> RequiredPrivileges { get; }

		OutputType Execute(Pipeline<InputType, OutputType> pipeline, InputType input);
	}
}
