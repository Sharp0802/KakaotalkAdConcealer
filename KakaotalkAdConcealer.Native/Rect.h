#pragma once

namespace KakaotalkAdConcealer::Native
{
	public value struct Rect
	{
	private:
		long _top;
		long _left;
		long _bottom;
		long _right;

	public:
		Rect(const long top, const long left, const long bottom, const long right);

		property long Top
		{
			long get() { return _top; }
		}
		property long Left
		{
			long get() { return _left; }
		}
		property long Bottom
		{
			long get() { return _bottom; }
		}
		property long Right
		{
			long get() { return _right; }
		}
	};
}