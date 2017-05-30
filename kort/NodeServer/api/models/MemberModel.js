'use strict';
var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var MemberSchema = new Schema(
{
	NickName:
	{
		type: String
	},
	CreatedDate:
	{
		type: Date,
		default: Date.now
	},
	CardId:
	{
		type: String
	}
});

module.exports = mongoose.model('Member', MemberSchema);