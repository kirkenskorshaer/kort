'use strict';
module.exports = function(app)
{
	var Service = require('../controllers/ServiceController');

	app.route('/Services/:memberId')
		.get(Service.getServicesOnMember);

	app.route('/Service')
		.post(Service.insertService)
		.put(Service.updateService);

	app.route('/Service/:serviceId')
		.delete(Service.deleteService);
};