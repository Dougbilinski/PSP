import { IProperty } from 'interfaces';
import queryString from 'query-string';
import React from 'react';
import { Link } from 'react-router-dom';
import { CellProps } from 'react-table';

/**
 * A cell that provides a clickable link to view a given IProperty
 * @param {CellProps<IProperty, number>} props
 */
const ViewPropertyCell = (props: CellProps<IProperty, number>) => {
  const property = props.row.original;
  return (
    <Link
      target="_blank"
      rel="noopener noreferrer"
      to={{
        pathname: `/mapview`,
        search: queryString.stringify({
          sidebar: true,
          disabled: true,
          loadDraft: false,
          parcelId: property.id,
        }),
      }}
    >
      View
    </Link>
  );
};

export default ViewPropertyCell;
