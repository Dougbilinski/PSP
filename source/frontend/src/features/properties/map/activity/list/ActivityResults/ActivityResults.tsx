import { Table } from 'components/Table';
import { TableSort } from 'components/Table/TableSort';
import { Api_Activity } from 'models/api/Activity';
import { Api_PropertyFile } from 'models/api/PropertyFile';

import { createActivityTableColumns } from './columns';

export interface IActivityResultProps {
  results: Api_Activity[];
  loading?: boolean;
  sort: TableSort<Api_Activity>;
  setSort: (value: TableSort<Api_Activity>) => void;
  onShowActivity: (activity: Api_Activity) => void;
  onDelete: (activity: Api_Activity) => void;
  getFilePropertyIndexById: (propertyFileId: number) => number;
  fileProperties: Api_PropertyFile[];
}

export function ActivityResults(props: IActivityResultProps) {
  const { results, setSort, sort, ...rest } = props;

  const columns = createActivityTableColumns(
    props.onShowActivity,
    props.onDelete,
    props.getFilePropertyIndexById,
    props.fileProperties,
  );

  return (
    <Table<Api_Activity>
      name="ActivityTable"
      manualSortBy={false}
      lockPageSize={true}
      manualPagination={false}
      totalItems={results.length}
      columns={columns}
      externalSort={{ sort, setSort }}
      data={results ?? []}
      noRowsMessage="No matching Activity found"
      {...rest}
    ></Table>
  );
}