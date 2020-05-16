#include "statsdialog.hpp"

StatsDialog::StatsDialog(const QVector<App> &apps, const QString &mood,
	const QString &energy, const Bot &bot, QWidget *parent)
	: QDialog(parent)
{
	auto layout = new QVBoxLayout();
	layout->addWidget(new QLabel(QString("Mood: %1").arg(mood)));
	layout->addWidget(new QLabel(QString("Energy: %1").arg(energy)));

	auto table = new QTreeWidget(this);
	table->setHeaderLabels({
		"Name", "Time"
	});
	table->setColumnCount(2);
	table->setRootIsDecorated(false);

	for (auto &app : apps)
		table->addTopLevelItem(new QTreeWidgetItem({
			app.name, app.getTimeRunning()
		}));
	setLayout(layout);
	setWindowTitle("Stats");
}
