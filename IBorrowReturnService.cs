﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartbookstore
{
    public interface IBorrowReturnService
    {
        void BorrowBook();
        void ReturnBook();
    }

}