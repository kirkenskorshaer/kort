'use strict';

var mongoose = require('mongoose'),
	Service = mongoose.model('Service');

exports.getServicesOnMember = function(req, res)
{
	Service.find({"MemberId":req.params.memberId}, function(err, service)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(service);
	});
};

exports.insertService = function(req, res)
{
	var service = new Service(req.body);
	service.save(function(err, service)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(service);
	});
};

exports.updateService = function(req, res)
{
	Service.findOneAndUpdate(req.params.serviceId, req.body, {new: true}, function(err, service)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(service);
	});
};

exports.deleteService = function(req, res)
{
	Service.remove({_id: req.params.serviceId}, function(err, service)
	{
		if (err)
		{
			res.send(err);
		}
		res.json(service);
	});
};