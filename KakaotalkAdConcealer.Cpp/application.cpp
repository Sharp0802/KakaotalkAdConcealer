#include "application.h"

#define _CRT_SECURE_NO_WARNINGS

int APIENTRY WinMain(HINSTANCE, HINSTANCE, LPSTR, int)
{
	try
	{
		return application().Main();
	}
	catch (const std::exception& exception)
	{
		const size_t size = strlen(exception.what()) + 1;
		std::unique_ptr<wchar_t> message(new wchar_t[size]);
		mbstowcs(message.get(), exception.what(), size);
		std::wstringstream stream{};
		stream << L"Unexpected exception occurred: \"" << message << L"\"";
		MessageBoxW(nullptr, stream.str().c_str(), L"Fatal error", MB_ICONERROR);
	}
}

application::application()
{
}

int application::Main()
{
	auto cancellation = false;
	auto thread = remove_all_embedded_ad(&cancellation);
	thread.join();
	return 0;
}
