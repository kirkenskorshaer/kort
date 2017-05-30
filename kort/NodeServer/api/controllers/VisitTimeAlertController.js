'use strict';

var mongoose = require('mongoose'),
	VisitTimeAlert = mongoose.model('VisitTimeAlert');

exports.getVisitTimeAlertsOnMember = function(req, res)
{
	VisitTimeAlert.find({"MemberId":req.params.memberId}, function(err, visitTimeAlert)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(visitTimeAlert);
	});
};

exports.insertVisitTimeAlert = function(req, res)
{
	var visitTimeAlert = new VisitTimeAlert(req.body);
	visitTimeAlert.save(function(err, visitTimeAlert)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(visitTimeAlert);
	});
};

exports.deleteVisitTimeAlert = function(req, res)
{
	VisitTimeAlert.remove({_id: req.params.visitTimeAlertId}, function(err, visitTimeAlert)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(visitTimeAlert);
	});
};