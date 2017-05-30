'use strict';

module.exports = function(app)
{
	var VisitTimeAlert = require('../controllers/VisitTimeAlertController');

	app.route('/VisitTimeAlerts/:memberId')
		.get(VisitTimeAlert.getVisitTimeAlertsOnMember);

	app.route('/VisitTimeAlert/:visitTimeAlertId')
		.delete(VisitTimeAlert.deleteVisitTimeAlert);

	app.route('/VisitTimeAlert')
		.post(VisitTimeAlert.insertVisitTimeAlert);
};