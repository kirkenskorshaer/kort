'use strict';


var mongoose = require('mongoose'),
	Member = mongoose.model('Member');

exports.getMembersByCardId = function(req, res)
{
	Member.find({"CardId":req.params.cardId}, function(err, member)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(member);
	});
};

exports.getMembersByNickName = function(req, res)
{
	Member.find({"NickName":req.params.nickName}, function(err, member)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(member);
	});
};

exports.getMembersToAlert = function(req, res)
{
	Member.find({}, function(err, member)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(member);
	});
};

exports.insertMember = function(req, res)
{
	var member = new Member(req.body);
	member.save(function(err, member)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(member);
	});
};

exports.updateMember = function(req, res)
{
	Member.findOneAndUpdate(req.params.memberId, req.body, {new: true}, function(err, member)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(member);
	});
};

exports.deleteMember = function(req, res)
{
	Member.remove({_id: req.params.memberId}, function(err, member)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(member);
	});
};