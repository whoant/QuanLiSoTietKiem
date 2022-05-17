using QuanLiSoTietKiem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLiSoTietKiem.Helpers
{
    public static class GeneratorPdf
    {
        public static byte[] Generator(string html)
        {
            var Renderer = new IronPdf.ChromePdfRenderer();
            var PDF = Renderer.RenderHtmlAsPdf(html).BinaryData;
            return PDF;
        }

        public static string DepositHtml(Deposit deposit)
        {
            return $"<!DOCTYPE html><html lang='vi'><head><meta charset='UTF - 8'><title>Title</title></head><body><span class='im'> <table> <tbody> <tr> <td style='padding-top:9px'> <table style='max-width:100%;min-width:100%'> <tbody> <tr> <td style='padding:0px 18px 9px;font-size:15px;line-height:150%' valign='top'> <p style='font-size:15px;line-height:150%'><span style='font-size:15px'><span style='color:#563d82'>{deposit.Name}<strong></strong></span><span style='color:#000000'> thân mến</span>,<br><br>Cảm ơn Quý khách đã lựa chọn sử dụng sản phẩm Tiết kiệm<br>Chúng tôi rất vui được xác nhận rằng Quý khách vừa mở thành công<strong> {deposit.Type}</strong> với các thông tin như sau:</span></p> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </span> <table border='0' cellpadding='0' cellspacing='0' width='100%'> <tbody> <tr> <td valign='top'> <table border='1' cellpadding='10px' style='font:15px helvetica;text-align:center;width:100%;border-color:#979797'> <tbody> <tr> <td colspan='2'> <p align='center'><span class='im'>Số tài khoản tiết kiệm<br></span><strong style='font-size:15px'>{deposit.SavingBookId}</strong></p> </td> </tr> <tr> <td colspan='2'> <p align='center'>Số tiền gửi<br><strong style='font-size:15px'>{deposit.DepositAmount}</strong></p> </td> </tr> <tr> <td width='50%'>Ngày mở<br><strong>{deposit.EffectedAt}</strong></td> <td width='50%'>Ngày đến hạn<br><strong>{deposit.ExpirationAt}</strong></td> </tr> <tr> <td width='50%'>Kỳ hạn & Lãi cuối kỳ<br><strong>{deposit.TypeDeposit}</strong></td> <td width='50%'>Số tiền lãi<br><strong>{deposit.InterestAmount}</strong></td> </tr> <tr> <td colspan='2' style='background-color:#efefef'> <p align='center'><span class='im'>Số dư vào ngày đến hạn<br></span><strong style='font-size:18px'>{deposit.TotalAmount}</strong></p> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table></body></html>";
        }

        public static string BillHtml(Bill bill)
        {
            return $"<!DOCTYPE html><html lang='en' xmlns='http://www.w3.org/1999/html'><head> <meta charset='UTF-8'> <title>Title</title></head><body><span class='im'> <table> <tbody> <tr> <td style='padding-top:9px'> <table style='max-width:100%;min-width:100%'> <tbody> <tr> <td style='padding:0px 18px 9px;font-size:15px;line-height:150%' valign='top'> <p style='font-size:15px;line-height:150%'> <span style='font-size:15px'> <span style='color:#563d82'> <strong>{bill.Name}</strong></span><span style='color:#000000'> thân mến</span>,<br /><br />Cảm ơn Quý khách đã lựa chọn sử dụng sản phẩm Tiết kiệm<br />Chúng tôi rất vui được đồng hành với quý khách vào thời gian vừa qua<br /> <br /> Ngày xuất phiếu: <strong>{bill.ClosingAt}</strong> <br /> Số tài khoản: <strong>{bill.SavingBookId}</strong> <br /> Số tiền gửi: <strong>{bill.DepositAmount}</strong> <br /> Số tiền lãi: <strong>{bill.InterestAmount}</strong> <br /> Ngày gửi: <strong>{bill.EffectedAt}</strong> <br /> Lãi suất: <strong>{bill.Month}</strong> </span> </p> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </span></body></html>";
        }
    }
}