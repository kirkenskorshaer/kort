'use strict';
module.exports = function(app)
{
	var Member = require('../controllers/MemberController');

	app.route('/Members/CardId/:cardId')
		.get(Member.getMembersByCardId);

	app.route('/Members/NickName/:nickName')
		.get(Member.getMembersByNickName);

	app.route('/Members/ToAlert')
		.get(Member.getMembersToAlert);

	app.route('/Member')
		.post(Member.insertMember)
		.put(Member.updateMember);

	app.route('/Member/:memberId')
		.delete(Member.deleteMember);
};