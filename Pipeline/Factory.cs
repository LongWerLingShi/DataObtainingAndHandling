using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Pipeline
{
    class Factory
    {
        private Page mypage;
        private Preview mypre;
        public Factory()
        {
            mypage = new Page();
            mypre = new Preview();
        }
        public ArrayList getPage(string title = "")
        {
            return mypage.getPage(title);
        }
        public bool savePage(PageItem item)
        {
            return mypage.savePage(item);
        }
        public ArrayList getPreview(string title = "")
        {
            return mypre.getPreview(title);
        }
        public bool savaPreview(PreviewItem item)
        {
            return mypre.savePreview(item);
        }
    }
}
