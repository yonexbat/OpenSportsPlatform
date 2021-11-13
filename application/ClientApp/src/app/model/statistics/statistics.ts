import { MonthValueItem } from './monthValueItem';
import { StatisticsItem } from './statisticsItem';

export interface Statistics {
    monthItems: StatisticsItem[];
    yearItems: StatisticsItem[];
    runningLast12Months: MonthValueItem[];
}
