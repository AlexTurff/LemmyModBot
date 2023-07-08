# LemmyModBot
A moderation bot for the Lemmy platform

Currently just runs as an executable.

#Configuration
**LemmyApiHost** - Enter the Lemmy API URI eg. https://lemmy.world/api/v3
**LemmyUsername** - The username for the bot to run as. Only supports an account on the same lemmy instance
**LemmyPassword** - plaintext password for the lemmy account



**PollDelaySeconds** - time in seconds after each batch that the bot will wait before searching for new posts/comments
**RefreshCredentialMinutes** - how often the bot reauthenticates - have not yet tested the default expiry so this is a workaround.



Next, under Communities -> List you need to configure what the bot will do in each community.



**Name** - the name of the community (as found in the URL)
**Tasks** - the list of Tasks the bot will do in that community.



Tasks:

**Name** - the name of the task to run. Supported tasks are: `RequireTag, RequireSpoilersIfTagged, MatchesRegex, DoesntMatchRegex`

**ContentType** - what content the task will run against. Some tasks will only run for some content types and ignore others. Supported content types are: `None, PostTitle, PostBody, Comment, PostTitlesAndComments = (PostTitle and Comment), PostBodiesAndComments = (PostBody and Comment), Posts = (PostBody and PostTitle)`

**Active** - if the task is enabled

**Actions** - The list of actions that should be applied if the task finds content to action. Supported actions are: `MessageUser, AddComment, Report, Remove`

**Comment** - If the comment action is applied, this is the comment that will be posted.

**ReportOrRemoveComment** - if the Remove or Report actions are applied, this will be the report/removal reason.

**PrivateMessageContent** - if the MessageUser action is applied, this is the message that will get sent to the user. the post/comment URL will be appened to the end of the message.

**RegularExpression** - this is the regular expression used by the MatchesRegex and DoesntMatchRegex tasks.

**OnlyActionIfPostTitleMatchRegex** - Filter that can be applied to tasks - the Task will additionally only match if the regex in this value matches the Post title (works for posts and comments).


Example Config:
```
{
	"LemmyApiHost": "",
	"LemmyUsername": "",
	"LemmyPassword": "",

	"PollDelaySeconds": 60,
	"RefreshCredentialMinutes": 30,

	"Communities": {
		"List": [
			{
				"Name": "exampleCommunityName",
				"Tasks": [
					{
						"Name": "RequireTag",
						"ContentType": "PostTitle",
						"Active": true,
						"Actions": [ "AddComment", MessageUser, Report, Remove ],
						"Comment": "Please edit its title to include a tag such as [No Spoilers], [Spoiler] etc to indicate the level of spoilers expected within.",
						"ReportOrRemoveComment": "Post missing spoiler tag.",
						"PrivateMessageContent": "One of your posts is missing a spoiler [tag], please edit its title to include a tag such as [No Spoilers], [Spoiler] etc to indicate the level of spoilers expected within."
					},
					{
						"Name": "MatchesRegex",
						"ContentType": "Comment",
						"Active": true,
						"Actions": [ "AddComment" ],
						"Comment": "GNU Terry Pratchett",
						"RegularExpression": "Terry Pratchett",
						"OnlyActionIfPostTitleMatchRegex": "\\[(TP|Terry Pratchett)\\]"
					},
					{
						"Name": "RequireSpoilersIfTagged",
						"ContentType": "PostBody",
						"Active": true,
						"Actions": [ "AddComment" ],
						"Comment": "You have indicated that this post includes spoilers, but have not included any spoiler tags in the post. If your post content contains spoilers, please wrap them in spoiler markup"
					}
				]
			}
		]
	}
}
```
