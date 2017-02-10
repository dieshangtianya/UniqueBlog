using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniqueBlog.DTO;
using UniqueBlog.Service.Interfaces;
using UniqueBlog.Controllers.ResponseResults;
using UniqueBlog.Controllers.Models.ViewModels;

namespace UniqueBlog.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PostCommentController:BlogControllerBase
    {
        private IPostCommentService _postCommentService;

        [ImportingConstructor]
        public PostCommentController(IPostCommentService postCommentService)
        {
            _postCommentService = postCommentService;
        }

        [HttpPost]
        public ActionResult PublishComment(string nickName,string comment, int postId)
        {
            PostCommentDto commentDto = new PostCommentDto();
            commentDto.UserName = nickName;
            commentDto.CommentContent = comment;
            commentDto.CreatedDate = DateTime.Now;
            commentDto.Post = new PostDto();
            commentDto.Post.Id = postId;
            commentDto.BlogId = CommonBlogData.CurrentInstance.BlogInformation.Id;
            

            bool flag = this._postCommentService.PublishComment(commentDto);

            ResponseJsonResult responseJsonResult = new ResponseResults.ResponseJsonResult(flag);
            if (!flag)
            {
                responseJsonResult.Message = "There is an error happen while publish the comment";
            }

            return Json(responseJsonResult);
        }

        public ActionResult PostCommentList(int postId)
        {
            var postCommentList = this._postCommentService.GetPostCommentListByPostId(postId);

            var postCommentListVM = new PostCommentListViewModel(postCommentList);
            postCommentListVM.HasUserLogin = this.IsUserLogin();

            return PartialView(postCommentListVM);
        }

        [HttpPost]
        public ActionResult DeleteComment(int commentId)
        {
            bool flag = this._postCommentService.DeleteComment(commentId);

            ResponseJsonResult responseJsonResult = new ResponseResults.ResponseJsonResult(flag);
            if (!flag)
            {
                responseJsonResult.Message = "There is an error happen while delete the comment";
            }

            return Json(responseJsonResult);
        }
    }
}
