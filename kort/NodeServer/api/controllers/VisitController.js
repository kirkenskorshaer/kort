'use strict';

var mongoose = require('mongoose'),
	Visit = mongoose.model('Visit');

exports.getVisitsOnMember = function(req, res)
{
	Visit.find({"MemberId":req.params.memberId}, function(err, visit)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(visit);
	});
};

exports.insertVisit = function(req, res)
{
	var visit = new Visit(req.body);
	visit.save(function(err, visit)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(visit);
	});
};
