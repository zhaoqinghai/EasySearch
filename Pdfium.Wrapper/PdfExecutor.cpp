#include "pch.h"

#include "PdfExecutor.h"
#include <fpdfview.h>
#include <fpdf_text.h>
using namespace msclr::interop;
int PdfiumWrapper::PdfExecutor::GetTotalPageCount()
{
    if (_pdf == nullptr) {
        return 0;
    }
    return FPDF_GetPageCount(_pdf);
}
String^ PdfiumWrapper::PdfExecutor::GetPageText(int pageNo)
{
    auto page = FPDF_LoadPage(_pdf, pageNo);
    if (page == nullptr) {
        return String::Empty;
    }
    auto textSeeker = FPDFText_LoadPage(page);
    if (textSeeker == nullptr) {
        FPDF_ClosePage(page);
        return String::Empty;
    }
    const int charCount = FPDFText_CountChars(textSeeker) + 1;

    std::unique_ptr<unsigned short[]> buffer = std::make_unique<unsigned short[]>(charCount);
    FPDFText_GetText(textSeeker, 0, charCount, buffer.get());
    FPDFText_ClosePage(textSeeker);
    FPDF_ClosePage(page);

    return marshal_as< String^>(reinterpret_cast<wchar_t*>(buffer.get()));
}