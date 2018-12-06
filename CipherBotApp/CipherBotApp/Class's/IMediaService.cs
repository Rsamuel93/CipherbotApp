using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CipherBot.Class_s
{
   
        public interface IMediaService

        {

            Task OpenGallery();

            void ClearFiles(List<string> filePaths);

        }
    
}
