using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;

namespace ThietKeWeb.Models.ViewModel
{
    public class HomeProductVM
    {
        //tiêu chí để search theo tên, mô tả sp
        // hoặc loại sản phẩm
        public string SearchTerm { get; set; }

        //Các thuộc tính hỗ trợ phân trang
        public int PageNumber { get; set; }//trang hiện tại
        public int PageSize { get; set; } = 6; //số sp mỗi trang

        
        public List<Product> Products { get; set; }
        
        
        public PagedList.IPagedList<Product> BestSellerProducts { get; set; }
        
    }
}