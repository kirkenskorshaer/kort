'use strict';

module.exports = function(app)
{
	var Visit = require('../controllers/VisitController');

	app.route('/Visits/:memberId')
		.get(Visit.getVisitsOnMember);

	app.route('/Visit')
		.post(Visit.insertVisit);
};