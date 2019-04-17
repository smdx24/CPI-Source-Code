// Times
using System;

public sealed class Times
{
	private static long NanosecondsPerTick = 100L;

	public static long NanoTime()
	{
		return DateTime.UtcNow.Ticks * NanosecondsPerTick;
	}
}
