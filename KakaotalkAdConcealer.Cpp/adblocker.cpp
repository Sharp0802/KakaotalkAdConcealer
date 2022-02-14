#include "adblocker.h"

void remove_popup()
{
	auto popup = FindWindowExW(nullptr, nullptr, nullptr, L"");

	std::wstring class_name(128, L'\0');
	GetClassNameW(popup, const_cast<wchar_t*>(class_name.c_str()), 128);
	if (GetParent(popup) != nullptr || class_name.find(L"RichPopWnd") == std::wstring::npos)
		return;

	RECT rect;
	if (!GetWindowRect(popup, &rect))
		return;

	if (rect.right - rect.left == 300 &&
		rect.bottom - rect.top == 150)
		SendMessageW(popup, WM_CLOSE, 0, 0);
}

std::thread remove_all_embedded_ad(volatile bool* cancellation_required)
{
	std::thread worker([=] 
		{
			while (!*cancellation_required)
			{
				PROCESSENTRY32W entry{};
				entry.dwSize = sizeof(entry);
				HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, NULL);

				if (Process32FirstW(snapshot, &entry) == true)
				{
					while (Process32NextW(snapshot, &entry) == true)
					{
						if (wcscmp(entry.szExeFile, L"KakaoTalk.exe") != 0) continue;

						std::pair<HWND, DWORD> params = { 0, entry.th32ProcessID };
						auto result = EnumWindows([](HWND hwnd, LPARAM lparam) -> BOOL
							{
								auto params = reinterpret_cast<std::pair<HWND, DWORD>*>(lparam);
								DWORD process_id;
								if (GetWindowThreadProcessId(hwnd, &process_id) && process_id == params->second)
								{
									params->first = hwnd;
									return false;
								}
								return true;
							}, reinterpret_cast<LPARAM>(&params));
						if (!result && params.first)
						{
							remove_embedded_ad(params.first, cancellation_required);
						}
					}
				}

				CloseHandle(snapshot);

				std::this_thread::sleep_for(std::chrono::milliseconds(update_rate));
			}
		});

	return worker;
}

void remove_embedded_ad(HWND kakaotalk, volatile bool* cancellation_requested)
{
	if (kakaotalk == nullptr) return;

	std::vector<HWND> children;
	EnumChildWindows(kakaotalk, [](HWND hwnd, LPARAM lparam) -> BOOL
		{
			reinterpret_cast<std::vector<HWND>*>(lparam)->push_back(hwnd);
			return true;
		}, reinterpret_cast<LPARAM>(&children));

	RECT rect;
	GetWindowRect(kakaotalk, &rect);

	std::wstring class_name(128, L'\0');
	std::wstring caption(128, L'\0');
	for (auto& child : children)
	{
		if (*cancellation_requested)
			break;
		if (GetParent(child) != kakaotalk)
			continue;

		GetClassNameW(child, const_cast<wchar_t*>(class_name.c_str()), 128);
		GetWindowTextW(child, const_cast<wchar_t*>(caption.c_str()), 128);

		UpdateWindow(kakaotalk);

		hide_main_window_ad(class_name, kakaotalk, child);
		hide_main_view_ad(caption, rect, child);
		hide_lock_view_ad(caption, rect, child);
	}
}

void hide_main_window_ad(const std::wstring& class_name, HWND kakaotalk, HWND child)
{
	if (class_name.compare(L"BannerAdWnd") != 0 && class_name.compare(L"EVA_Window") != 0)
		return;

	auto text_length = GetWindowTextLengthW(kakaotalk);
	std::wstring text(text_length, L'\0');
	GetWindowTextW(kakaotalk, const_cast<wchar_t*>(text.c_str()), text_length);
	if (text.rfind(L"LockModeView", 0) != std::wstring::npos)
		return;

	ShowWindow(child, HIDE_WINDOW);
	SetWindowPos(child, nullptr, 0, 0, 0, 0, SWP_NOMOVE);
}

void hide_main_view_ad(const std::wstring& caption, RECT rect, HWND child)
{
	if (caption.rfind(L"OnlineMainView", 0) == std::wstring::npos)
		return;
	SetWindowPos(child, nullptr, 0, 0, rect.right - rect.left - shadow_padding, rect.bottom - rect.top - main_view_padding, SWP_NOMOVE);
}

void hide_lock_view_ad(const std::wstring& caption, RECT rect, HWND child)
{
	if (caption.rfind(L"LockModeView", 0) == std::wstring::npos)
		return;
	SetWindowPos(child, nullptr, 0, 0, rect.right - rect.left - shadow_padding, rect.bottom - rect.top, SWP_NOMOVE);
}
