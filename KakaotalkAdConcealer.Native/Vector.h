#pragma once

namespace KakaotalkAdConcealer::Native
{
	public value struct Vector
	{
	private:
		int _x;
		int _y;
	public:
		Vector(int x, int y);

		property int X { int get() { return _x; } }
		property int Y { int get() { return _y; } }
	};
}
