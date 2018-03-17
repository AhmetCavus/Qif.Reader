//
//  DefaultRepository.cs
//
//  Author:
//       ahc <ahmet.cavus@wfp2.com>
//
//  Copyright (c) 2017 (c) Ahmet Cavus

namespace Cinary.Framework.Data.Modifier
{
	public abstract class AbstractModifier<TSource, TDestination> : IModifier
	{
		public abstract TDestination Modify(params TSource [] parameters);
	}

}