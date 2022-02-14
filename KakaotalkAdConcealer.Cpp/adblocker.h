#pragma once

#include "framework.h"

const int32_t update_rate = 100;
const int32_t shadow_padding = 2;
const int32_t main_view_padding = 31;

void remove_popup();

std::thread remove_all_embedded_ad(volatile bool* cancellation_required);

void remove_embedded_ad(HWND kakaotalk, volatile bool* cancellation_requested);

void hide_main_window_ad(const std::wstring& class_name, HWND kakaotalk, HWND child);

void hide_main_view_ad(const std::wstring& caption, RECT rect, HWND child);

void hide_lock_view_ad(const std::wstring& caption, RECT rect, HWND child);