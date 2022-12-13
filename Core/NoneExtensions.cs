// Copyright (c) 2019 under MIT license.

using OneOf;
using OneOf.Types;

namespace Core;

public static class NoneExtensions
{
    public static OneOf<T, None> Of<T>(this None _) => (OneOf<T, None>)default(None);
}
