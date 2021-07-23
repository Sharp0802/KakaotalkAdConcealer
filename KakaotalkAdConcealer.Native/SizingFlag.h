#pragma once

namespace KakaotalkAdConcealer::Native
{
	public enum struct SizingFlag : int
	{
		NoSize = 1 << 0,
		NoMove = 1 << 1,
		NoZOrder = 1 << 2,
		NoReDraw = 1 << 3,
		NoActivate = 1 << 4,
		DrawFrame = 1 << 5,
		ShowWindow = 1 << 6,
		HideWindow = 1 << 7,
		NoCopyBits = 1 << 8,
		NoOwnerZOrder = 1 << 9,
		NoSendChanging = 1 << 10,
		DeferErase = 1 << 11,
		AsyncWindowPos = 1 << 12
	};
}