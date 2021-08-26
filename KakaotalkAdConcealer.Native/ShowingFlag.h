#pragma once

#include <cstdint>

namespace KakaotalkAdConcealer::Native
{
	public enum struct ShowingFlag : int
	{
		Hide = 0,
		ShowNomal = 1,
		ShowMinimized = 2,
		ShowMaximize = 3,
		ShowNoActivate = 4,
		Show = 5,
		Minimize = 6,
		ShowMinNoActive = 7,
		ShowNA = 8,
		Restore = 9,
		SHowDefault = 10,
		ForceMinimize = 11
	};
}