#pragma once
#include <codecvt>
#include <locale>
#include <string>
#include <fpdfview.h>

using namespace System;
using namespace System::Collections::Generic;
using namespace msclr::interop;
namespace PdfiumWrapper {
    public ref class PdfExecutor
    {
    private:

        String^ _filePath;
        FPDF_DOCUMENT _pdf;
        std::string ToUtf8(const std::wstring& wideStr) {
            std::wstring_convert<std::codecvt_utf8<wchar_t>> converter;
            return converter.to_bytes(wideStr);
        }
    public:
        PdfExecutor(String^ path) : _filePath(path) {
            FPDF_InitLibrary();
            marshal_context context;
            auto file = context.marshal_as<std::wstring>(_filePath);
            std::wstring_convert<std::codecvt_utf8<wchar_t>> converter;
            _pdf = FPDF_LoadDocument(converter.to_bytes(file).c_str(), nullptr);
        }

        bool IsInValid() {
            return _pdf == nullptr;
        }

        ~PdfExecutor() {
            if (_pdf != nullptr) {
                FPDF_CloseDocument(_pdf);
            }
            FPDF_DestroyLibrary();
        }

        int GetTotalPageCount();

        String^ GetPageText(int pageNo);
    };
}
